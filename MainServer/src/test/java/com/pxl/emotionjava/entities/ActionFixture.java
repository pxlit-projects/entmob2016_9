package com.pxl.emotionjava.entities;

public class ActionFixture {
    public static Action anAction() {
        Action action = new Action();
        action.setAction("VOLUME_UP");
        return action;
    }

    public static Action anAction(int id) {
        Action action = anAction();
        action.setActId(id);
        return action;
    }
}
