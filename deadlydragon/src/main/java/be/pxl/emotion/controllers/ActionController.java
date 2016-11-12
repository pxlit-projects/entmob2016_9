package be.pxl.emotion.controllers;

import be.pxl.emotion.beans.Action;
import be.pxl.emotion.beans.Command;
import be.pxl.emotion.services.ActionDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(value="/action", produces="application/json")
@Component
public class ActionController {
    @Autowired
    ActionDataService actionService;

    //Lijst van alle profiles opvragen
    @RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
    public List<Action> getActions() {
        List<Action> listOfActions = actionService.getAllActions();
        return listOfActions;
    }

    // Een actionlist opvragen aan de hand van id (json profile)
    @RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
    public Action getActionById(@PathVariable int id) {
        return actionService.getActionById(id);
    }

    // action toevoegen (json action)
    @RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
    public Action addProfile(@RequestBody Action action) {
        return actionService.addAction(action);
    }

    // verwijder action adhv id
    @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
    public String deleteAction(@PathVariable("id") int id) {
        return actionService.deleteAction(id);
    }

    // Actionlijst in profile aanpassen adhv id (json action, action)
    @RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
    public Action updateAction(@RequestBody Action action) {
        return actionService.updateAction(action);
    }

}
