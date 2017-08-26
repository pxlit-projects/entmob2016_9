package com.pxl.emotionjava.entities;

import javax.persistence.*;

@Entity
public class Command {
	@Id
	@Column(name = "id")
	private int commandId;
    @Column(name = "command")
	private String command;

    public int getCommandId() {
        return commandId;
    }

    public void setCommandId(int commandId) {
        this.commandId = commandId;
    }

    public String getCommand() {
        return command;
    }

    public void setCommand(String command) {
        this.command = command;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Command)) return false;

        Command command1 = (Command) o;

        return getCommand().equals(command1.getCommand());
    }

    @Override
    public int hashCode() {
        int result = getCommandId();
        result = 31 * result + getCommand().hashCode();
        return result;
    }
}
