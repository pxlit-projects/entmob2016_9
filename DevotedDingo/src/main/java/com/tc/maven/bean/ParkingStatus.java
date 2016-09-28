package com.tc.maven.bean;

import java.io.Serializable;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="ParkingStatusses")
public class ParkingStatus implements Serializable{
	
	@Id
	@GeneratedValue
	private int id;
	@Column(name="AvailableCapacity")
	private int availableCapacity;
	@Column(name="TotalCapacity")
	private int totalCapacity;
	@Column(name="Open")
	private boolean open;
	@Column(name="SuggestedCapacity")
	private String suggestedCapacity;
	@Column(name="ActiveRoute")
	private String activeRoute;
	@Column(name="LastModifiedDate")
	private Date lastModifiedDate;
	
	public int getAvailableCapacity() {
		return availableCapacity;
	}
	public void setAvailableCapacity(int availableCapacity) {
		this.availableCapacity = availableCapacity;
	}
	public int getTotalCapacity() {
		return totalCapacity;
	}
	public void setTotalCapacity(int totalCapacity) {
		this.totalCapacity = totalCapacity;
	}
	public boolean isOpen() {
		return open;
	}
	public void setOpen(boolean open) {
		this.open = open;
	}
	public String getSuggestedCapacity() {
		return suggestedCapacity;
	}
	public void setSuggestedCapacity(String suggestedCapacity) {
		this.suggestedCapacity = suggestedCapacity;
	}
	public String getActiveRoute() {
		return activeRoute;
	}
	public void setActiveRoute(String activeRoute) {
		this.activeRoute = activeRoute;
	}
	public Date getLastModifiedDate() {
		return lastModifiedDate;
	}
	public void setLastModifiedDate(String lastModifiedDateString) throws ParseException {
		DateFormat df = new SimpleDateFormat("dd/MM/yyyy hh:mm:ss");
		this.lastModifiedDate = df.parse(lastModifiedDateString);
	}
}
