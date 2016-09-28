package com.tc.maven.bean;

import java.io.Serializable;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="Days")
public class Day implements Serializable{

	@Id
	@GeneratedValue
	private int id;
	
	@Column(name="Day")
	private String day;

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getDay() {
		return day;
	}

	public void setName(String day) {
		this.day = day;
	}
	
	
}
