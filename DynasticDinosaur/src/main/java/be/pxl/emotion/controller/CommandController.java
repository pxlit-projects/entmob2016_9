package be.pxl.emotion.controller;

import java.util.List;

import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import be.pxl.emotion.bean.Command;
import be.pxl.emotion.service.CommandDataService;

@RestController
public class CommandController {
	CommandDataService commandService = new CommandDataService();
	
	// Een commandlist opvragen aan de hand van id (json profile)
		@RequestMapping(value = "/command/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
		public Command getCommandById(@PathVariable int id) {
			return commandService.getCommandById(id);
		}
}
