package com.turncount.zanyzebra.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import com.turncount.zanyzebra.entities.Action;
import com.turncount.zanyzebra.repositories.ActionRepository;

import java.util.List;

@Service("actionService")
@Component
public class ActionDataService {
	@Autowired
	private ActionRepository repo;
	
	public Action getActionById(int id)
	{
		return repo.findOne(id);
	}
	
	public Action updateAction(Action action) 
	{
		return repo.save(action);
	}
	
	public Action addAction(Action action) {
		return repo.save(action);
	}
	
	
	public String deleteAction(int id) {
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

	public List<Action> getAllActions() {
		return (List<Action>) repo.findAll();
	}
}
