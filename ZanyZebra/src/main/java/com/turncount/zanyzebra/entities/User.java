package com.turncount.zanyzebra.entities;

import javax.persistence.*;

@Entity
public class User {
	@Id
	@Column(name = "id")
	private int userId;
    @Column(name = "firstName")
	private String firstName;
    @Column(name = "lastName")
	private String lastName;
    @Column(name = "password")
	private String password;
    @Column(name = "userName")
	private String userName;
    @Column(name = "defaultProfileId")
    private int defaultProfileId;
    @Column(name="email")
    private String email;
    @Column(name="joinedOn")
    private String joinedOn;
    @Column(name="country")
    private String country;
    @Column(name="phone")
    private String phone;

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public int getDefaultProfileId() {
        return defaultProfileId;
    }

    public void setDefaultProfileId(int defaultProfileId) {
        this.defaultProfileId = defaultProfileId;
    }

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getJoinedOn() {
		return joinedOn;
	}

	public void setJoinedOn(String joinedOn) {
		this.joinedOn = joinedOn;
	}


	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone =phone;
	}
}
