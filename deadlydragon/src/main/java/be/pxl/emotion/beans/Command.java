package be.pxl.emotion.beans;

import javax.persistence.*;

@Entity
public class Command {
	@Id
	@Column(name = "Id")
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
}
