package com.turncount.zanyzebra.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import com.turncount.zanyzebra.entities.Action;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.services.ActionDataService;

import java.util.List;

@Controller
@RequestMapping(value="/action", produces="application/json")
public class ActionController {
    @Autowired
    ActionDataService actionService;
    @Autowired
	private JmsTemplate jmsTemplate;

    //Lijst van alle profiles opvragen
    @RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
    @ResponseBody
    public List<Action> getActions() {
        jmsTemplate.convertAndSend("log", new LogMessage("Request all actions", this.getClass().getName()));		
        List<Action> listOfActions = actionService.getAllActions();
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return listOfActions;
    }

    // Een actionlist opvragen aan de hand van id (json profile)
    @RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
    @ResponseBody
    public Action getActionById(@PathVariable int id) {
        return actionService.getActionById(id);
    }

    // action toevoegen (json action)
    @RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
    @ResponseBody
    public Action addAction(@RequestBody Action action) {
        jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Action %s", action.getActId()), this.getClass().getName()));		
        Action responseAction = actionService.addAction(action);
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return responseAction;
    }

    // verwijder action adhv id
    @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
    @ResponseBody
    public String deleteAction(@PathVariable("id") int id) {
  	  	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Delete for Action %s", id), this.getClass().getName()));		
  	  	String response = actionService.deleteAction(id);
  	  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
	  return response;
    }

    // Actionlijst in profile aanpassen adhv id (json action, action)
    @RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
    @ResponseBody
    public Action updateAction(@RequestBody Action action) {
  	  jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Update for User %s", action.getActId()), this.getClass().getName()));		
        Action responseAction = actionService.updateAction(action);
        jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
  	  return responseAction;
    }

}
