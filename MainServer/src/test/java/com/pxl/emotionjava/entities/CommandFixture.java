package com.pxl.emotionjava.entities;

public class CommandFixture {
    public static Command aCommand() {
        Command command = new Command();
        command.setCommand("UP");
        return command;
    }

    public static Command aCommand(int id) {
        Command command = aCommand();
        command.setCommandId(id);
        return command;
    }
}
