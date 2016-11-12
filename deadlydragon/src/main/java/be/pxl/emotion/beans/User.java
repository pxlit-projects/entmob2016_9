package be.pxl.emotion.beans;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

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
}
