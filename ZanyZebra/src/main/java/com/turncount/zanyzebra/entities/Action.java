package com.turncount.zanyzebra.entities;

import javax.persistence.*;

@Entity
public class Action {
	@Id
	@Column(name = "id")
	private int actId;
	@Column(name = "action")
	private String action;
	
	public Action(int actId, String action) {
		this.actId = actId;
		this.action  =action;
	}

	public int getActId() {
		return actId;
	}

	public void setActId(int actId) {
		this.actId = actId;
	}

	public String getAction() {
		return action;
	}

	public void setAction(String action) {
		this.action = action;
	}
}
