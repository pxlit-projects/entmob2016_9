package com.tc.maven.bean;

import java.io.Serializable;
import java.util.Arrays;
import java.util.Date;
import java.util.HashSet;
import java.util.Set;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import javax.persistence.Table;  

@Entity
@Table(name="Parkings")
public class Parking implements Serializable{
	@Id
	@Column(name="Id")
	private int id;
	@Column(name="LastModifiedDate")
	private Date lastModifiedDate;
	@Column(name="Name")
	private String name;
	@Column(name="Description")
	private String description;
	@Column(name="Latitude")
	private long latitude;
	@Column(name="Longitude")
	private long longitude;
	@Column(name="Address")
	private String address;
	@Column(name="ContactInfo")
	private String contactInfo;
	@Column(name="BlurAvailability")
	private String blurAvailability;
	@OneToOne
	@JoinColumn(name="city_id")
	private City city;
	@OneToOne
	@JoinColumn(name="parkingServer_id")
	private ParkingServer parkingServer;
	@Column(name="SuggestedFreeThreshold")
	private int suggestedFreeThreshold;
	@Column(name="SuggestedFullThreshold")
	private int suggestedFullThreshold;
	@Column(name="TotalCapacity")
	private int totalCapacity;
	@Column(name="CapacityRounding")
	private int capacityRounding;
	@OneToMany(mappedBy="parking")
	private Set<OpeningTimes> openingTimes;
	@OneToOne
	@JoinColumn(name="parkingStatus_id")
	private ParkingStatus parkingStatus;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Date getLastModifiedDate() {
		return lastModifiedDate;
	}
	public void setLastModifiedDate(Date lastModifiedDate) {
		this.lastModifiedDate = lastModifiedDate;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String description) {
		this.description = description;
	}
	public long getLatitude() {
		return latitude;
	}
	public void setLatitude(long latitude) {
		this.latitude = latitude;
	}
	public long getLongitude() {
		return longitude;
	}
	public void setLongitude(long longitude) {
		this.longitude = longitude;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getContactInfo() {
		return contactInfo;
	}
	public void setContactInfo(String contactInfo) {
		this.contactInfo = contactInfo;
	}
	public City getCity() {
		return city;
	}
	public void setCity(City city) {
		this.city = city;
	}
	public ParkingServer getParkingServer() {
		return parkingServer;
	}
	public void setParkingServer(ParkingServer parkingServer) {
		this.parkingServer = parkingServer;
	}
	public int getSuggestedFreeThreshold() {
		return suggestedFreeThreshold;
	}
	public void setSuggestedFreeThreshold(int suggestedFreeThreshold) {
		this.suggestedFreeThreshold = suggestedFreeThreshold;
	}
	public int getSuggestedFullThreshold() {
		return suggestedFullThreshold;
	}
	public void setSuggestedFullThreshold(int suggestedFullThreshold) {
		this.suggestedFullThreshold = suggestedFullThreshold;
	}
	public int getTotalCapacity() {
		return totalCapacity;
	}
	public void setTotalCapacity(int totalCapacity) {
		this.totalCapacity = totalCapacity;
	}
	public int getCapacityRounding() {
		return capacityRounding;
	}
	public void setCapacityRounding(int capacityRounding) {
		this.capacityRounding = capacityRounding;
	}
	public Set<OpeningTimes> getOpeningTimes() {
		return openingTimes;
	}
	public void setOpeningTimes(OpeningTimes[] openingTimes) {
		this.openingTimes = new HashSet<OpeningTimes>(Arrays.asList(openingTimes));
	}
	public ParkingStatus getParkingStatus() {
		return parkingStatus;
	}
	public void setParkingStatus(ParkingStatus parkingStatus) {
		this.parkingStatus = parkingStatus;
	}
	public String getBlurAvailability() {
		return blurAvailability;
	}
	public void setBlurAvailability(String blurAvailability) {
		this.blurAvailability = blurAvailability;
	}
}
