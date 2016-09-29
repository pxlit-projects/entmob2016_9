package com.tc.maven.bean;

import java.io.Serializable;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.Table;

@Entity
@Table(name="OpeningsTimes")
public class OpeningTimes implements Serializable{
	
	@Id
	@GeneratedValue
	private int id;
	@OneToMany(cascade=CascadeType.ALL, fetch=FetchType.LAZY)
	private Set<Day> days;
	@Column(name="From")
	private String from;
	@Column(name="To")
	private String to;
	
	@ManyToOne
	@JoinColumn(name="parking_id")
	private Parking parking;

	public String getFrom() {
		return from;
	}

	public void setFrom(String from) {
		this.from = from;
	}

	public String getTo() {
		return to;
	}

	public void setTo(String to) {
		this.to = to;
	}

	public Set<Day> getDays() {
		return days;
	}

	public void setDays(String[] days) {
		
		Set<Day> dayset = new HashSet<Day>();
		
		for (String d : Arrays.asList(days)) {
			Day day = new Day();
			day.setName(d);
			dayset.add(day);
		}
		
		this.days = dayset;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public Parking getParking() {
		return parking;
	}

	public void setParking(Parking parking) {
		this.parking = parking;
	}
}
