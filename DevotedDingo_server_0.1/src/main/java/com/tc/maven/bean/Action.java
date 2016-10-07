package com.tc.maven.bean;

import java.util.ArrayList;

public class Action {
	private int actId;
	private ArrayList<Integer> actions;
	public int getActId() {
		return actId;
	}
	public void setActId(int actId) {
		this.actId = actId;
	}
	public ArrayList<Integer> getActions() {
		return actions;
	}
	public void setActions(ArrayList<Integer> actions) {
		this.actions = actions;
	}
}
