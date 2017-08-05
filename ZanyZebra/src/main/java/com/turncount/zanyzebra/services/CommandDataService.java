package com.turncount.zanyzebra.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import com.turncount.zanyzebra.entities.Command;
import com.turncount.zanyzebra.repositories.CommandRepository;

import java.util.List;

@Service("commandService")
@Component
public class CommandDataService {
	@Autowired
	private CommandRepository repo;

	public Command getCommandById(int id) {
		return repo.findOne(id);
	}

	public Command updateCommand(Command command) {
		return repo.save(command);
	}

	public Command addCommand(Command command) {
		return repo.save(command);
	}

	public String deleteCommand(int id) {
        try {
            if (repo.exists(id)) {
                repo.delete(id);
                return "1";
            } else {
                return "2";
            }
        } catch (Exception e) {
            return e.getMessage();
        }
	}

    public List<Command> getAllCommands() {
		return (List<Command>) repo.findAll();
    }
}
