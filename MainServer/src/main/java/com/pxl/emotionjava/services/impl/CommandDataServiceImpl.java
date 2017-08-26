package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Command;
import com.pxl.emotionjava.repositories.CommandRepository;
import com.pxl.emotionjava.services.api.CommandDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service("commandService")
public class CommandDataServiceImpl implements CommandDataService {
	@Autowired
	private CommandRepository repo;

	@Override
	public Command getCommandById(int id) {
		return repo.findOne(id);
	}

	@Override
	public Command updateCommand(Command command) {
		return repo.save(command);
	}

	@Override
	public Command addCommand(Command command) {
		return repo.save(command);
	}

	@Override
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

	@Override
    public List<Command> getAllCommands() {
		return (List<Command>) repo.findAll();
    }
}
