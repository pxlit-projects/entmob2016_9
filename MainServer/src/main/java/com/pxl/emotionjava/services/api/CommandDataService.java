package com.pxl.emotionjava.services.api;

import com.pxl.emotionjava.entities.Command;

import java.util.List;

public interface CommandDataService {
    Command getCommandById(int id);

    Command updateCommand(Command command);

    Command addCommand(Command command);

    String deleteCommand(int id);

    List<Command> getAllCommands();
}
