package com.pxl.emotionjava.services.api;

import com.pxl.emotionjava.entities.Action;

import java.util.List;

public interface ActionDataService {
    Action getActionById(int id);

    Action updateAction(Action action);

    Action addAction(Action action);

    String deleteAction(int id);

    List<Action> getAllActions();
}
