package be.pxl.emotion.services;

import be.pxl.emotion.beans.Action;
import be.pxl.emotion.repositories.ActionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service("actionService")
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
}
