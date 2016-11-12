package be.pxl.emotion.beans;

import javax.persistence.*;

@Entity
public class Action {
	@Id
	@Column(name = "Id")
	private int actId;
	@Column(name = "action")
	private int action;

	public int getActId() {
		return actId;
	}

	public void setActId(int actId) {
		this.actId = actId;
	}

	public int getAction() {
		return action;
	}

	public void setAction(int action) {
		this.action = action;
	}
}
