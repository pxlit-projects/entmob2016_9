package com.turncount.zanyzebra.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;

import com.turncount.zanyzebra.entities.Command;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.services.CommandDataService;

import java.util.List;

@Controller
@RequestMapping(value="/command", produces="application/json")
public class CommandController {
	@Autowired
	CommandDataService commandService;
	@Autowired
	private JmsTemplate jmsTemplate;

	//Lijst van alle profiles opvragen
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Command> getCommands() {
        jmsTemplate.convertAndSend("log", new LogMessage("Request all commands", this.getClass().getName()));		
		List<Command> listOfCommands = commandService.getAllCommands();
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return listOfCommands;
	}
	
	// Een commandlist opvragen aan de hand van id (json profile)
		@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
		@ResponseBody
		public Command getCommandById(@PathVariable int id) {
	        jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Command %s", id), this.getClass().getName()));		
			Command command = commandService.getCommandById(id);
			jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
			return command;
		}
		
		// command toevoegen (json command)
		@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
		@ResponseBody
		public Command addProfile(@RequestBody Command command) {
			return commandService.addCommand(command);
		}
		
		// verwijder command adhv id
		@RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
		@ResponseBody
		public String deleteCommand(@PathVariable("id") int id) {
		      jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Adding Command %s", id), this.getClass().getName()));		
			String response = commandService.deleteCommand(id);
			jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
			  return response;
		}
		
		// Commandolijst in profile aanpassen adhv id (json command, action)
		@RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
		@ResponseBody
		public Command updateCommand(@RequestBody Command command) {
			  jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Update for Command %s", command.getCommandId()), this.getClass().getName()));		
			Command responseCommand = commandService.updateCommand(command);
			jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
			  return responseCommand;
		}
		
}
