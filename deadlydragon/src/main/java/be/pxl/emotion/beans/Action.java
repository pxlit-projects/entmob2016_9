package be.pxl.emotion.beans;

import javax.persistence.*;

@Entity
public class Action {
	@Id
	private int actId;
	private int act1;
	private int act2;
	private int act3;
	private int act4;
	public int getActId() {
		return actId;
	}
	public void setActId(int actId) {
		this.actId = actId;
	}
	public int getAct1() {
		return act1;
	}
	public void setAct1(int act1) {
		this.act1 = act1;
	}
	public int getAct2() {
		return act2;
	}
	public void setAct2(int act2) {
		this.act2 = act2;
	}
	public int getAct3() {
		return act3;
	}
	public void setAct3(int act3) {
		this.act3 = act3;
	}
	public int getAct4() {
		return act4;
	}
	public void setAct4(int act4) {
		this.act4 = act4;
	}
	
}
