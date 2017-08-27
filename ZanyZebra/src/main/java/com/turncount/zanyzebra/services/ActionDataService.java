package com.turncount.zanyzebra.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.turncount.zanyzebra.entities.Action;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.repositories.ActionRepository;

import java.util.List;

@Service("actionService")
@Component
@Transactional
public class ActionDataService {
	@Autowired
	private ActionRepository repo;
	
	@Autowired
	private JmsTemplate jmsTemplate;
	
	@Transactional(readOnly = true)
	public Action getActionById(int id)
	{
		try {
			return repo.findOne(id);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}
	
	@Transactional
	public Action updateAction(Action action) 
	{
		try {
			return repo.save(action);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}
	
	@Transactional
	public Action addAction(Action action) {
		try{
			return repo.save(action);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}
	
	@Transactional
	public String deleteAction(int id) {
        try {
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Searching for Action %s", id), this.getClass().getName()));	
            if (repo.exists(id)) {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Deleting Action %s", id), this.getClass().getName()));	
                repo.delete(id);
                return "1";
            } else {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Could not find Action %s", id), this.getClass().getName()));	
                return "2";
            }
        } catch (Exception e) {
            return e.getMessage();
        }
	}

	@Transactional(readOnly = true)
	public List<Action> getAllActions() {
		try {
			return (List<Action>) repo.findAll();
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}
}
