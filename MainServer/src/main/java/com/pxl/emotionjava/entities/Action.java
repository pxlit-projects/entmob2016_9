package com.pxl.emotionjava.entities;

import javax.persistence.*;

@Entity
public class Action {
	@Id
	@Column(name = "id")
	private int actId;
	@Column(name = "action")
	private String action;

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

	@Override
	public boolean equals(Object o) {
		if (this == o) return true;
		if (!(o instanceof Action)) return false;

		Action action1 = (Action) o;

		return getAction().equals(action1.getAction());
	}

	@Override
	public int hashCode() {
		int result = getActId();
		result = 31 * result + getAction().hashCode();
		return result;
	}
}
