package com.turncount.zanyzebra.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import com.turncount.zanyzebra.entities.Action;
import com.turncount.zanyzebra.services.ActionDataService;

import java.util.List;

@Controller
@RequestMapping(value="/action", produces="application/json")
public class ActionController {
    @Autowired
    ActionDataService actionService;

    //Lijst van alle profiles opvragen
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
    public Action addProfile(@RequestBody Action action) {
        return actionService.addAction(action);
    }

    // verwijder action adhv id
    @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
    @ResponseBody
    public String deleteAction(@PathVariable("id") int id) {
        return actionService.deleteAction(id);
    }

    // Actionlijst in profile aanpassen adhv id (json action, action)
    @RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
    @ResponseBody
    public Action updateAction(@RequestBody Action action) {
        return actionService.updateAction(action);
    }

}
