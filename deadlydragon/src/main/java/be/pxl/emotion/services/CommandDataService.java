package be.pxl.emotion.services;

import be.pxl.emotion.beans.Command;
import be.pxl.emotion.repositories.CommandRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service("commandService")
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
}
