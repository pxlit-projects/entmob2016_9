package com.turncount.zanyzebra.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.turncount.zanyzebra.entities.Command;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.repositories.CommandRepository;

import java.util.List;

@Service("commandService")
@Component
@Transactional
public class CommandDataService {
	@Autowired
	private CommandRepository repo;
	@Autowired
	private JmsTemplate jmsTemplate;

	@Transactional(readOnly = true)
	public Command getCommandById(int id) {
		try {
			return repo.findOne(id);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional
	public Command updateCommand(Command command) {
		try {
			return repo.save(command);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional
	public Command addCommand(Command command) {
		try {
			return repo.save(command);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional
	public String deleteCommand(int id) {
        try {
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Searching for Command %s", id), this.getClass().getName()));	
            if (repo.exists(id)) {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Deleting Command %s", id), this.getClass().getName()));	
                repo.delete(id);
                return "1";
            } else {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Could not find Command %s", id), this.getClass().getName()));	
                return "2";
            }
        } catch (Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
            return e.getMessage();
        }
	}

	@Transactional(readOnly = true)
    public List<Command> getAllCommands() {
		try {
			return (List<Command>) repo.findAll();
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
    }
}
