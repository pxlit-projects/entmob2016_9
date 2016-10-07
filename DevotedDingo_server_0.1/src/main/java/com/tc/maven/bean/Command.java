package com.tc.maven.bean;

import java.util.ArrayList;

public class Command {
	private int commandId;
	private ArrayList<Integer> commands;
	
	public int getCommandId() {
		return commandId;
	}
	public void setCommandId(int commandId) {
		this.commandId = commandId;
	}
	public ArrayList<Integer> getCommands() {
		return commands;
	}
	public void setCommands(ArrayList<Integer> commands) {
		this.commands = commands;
	}
}
