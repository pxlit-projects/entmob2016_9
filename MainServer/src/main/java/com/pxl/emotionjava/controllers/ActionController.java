package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.Action;
import com.pxl.emotionjava.services.api.ActionDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

import static org.springframework.http.HttpStatus.CREATED;

@RestController
@RequestMapping(value="/action", produces="application/json")
public class ActionController {
    @Autowired
    ActionDataService actionService;

    //Lijst van alle actions opvragen
    @RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
    @ResponseBody
    public List<Action> getActions() {
        List<Action> listOfActions = actionService.getAllActions();
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
    @ResponseStatus(CREATED)
    public Action addAction(@RequestBody Action action) {
        return actionService.addAction(action);
    }

    // verwijder action adhv id
    @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
    @ResponseBody
    public String deleteAction(@PathVariable("id") int id) {
        return actionService.deleteAction(id);
    }

    // Action aanpassen adhv id (json action, action)
    @RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
    @ResponseBody
    public Action updateAction(@RequestBody Action action) {
        return actionService.updateAction(action);
    }

}
