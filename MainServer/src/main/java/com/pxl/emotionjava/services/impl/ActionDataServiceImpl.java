package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Action;
import com.pxl.emotionjava.repositories.ActionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service("actionService")
public class ActionDataServiceImpl implements com.pxl.emotionjava.services.api.ActionDataService {
	@Autowired
	private ActionRepository repo;

	@Override
	public Action getActionById(int id)
	{
		return repo.findOne(id);
	}

	@Override
	public Action updateAction(Action action) 
	{
		return repo.save(action);
	}

	@Override
	public Action addAction(Action action) {
		return repo.save(action);
	}
	
	@Override
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

	@Override
	public List<Action> getAllActions() {
		return (List<Action>) repo.findAll();
	}
}
