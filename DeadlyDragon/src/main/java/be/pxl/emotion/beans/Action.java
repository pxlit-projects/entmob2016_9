package be.pxl.emotion.beans;

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
}
