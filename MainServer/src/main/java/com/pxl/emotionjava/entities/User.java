package com.pxl.emotionjava.entities;

import javax.persistence.*;

@Entity
@Table(name = "users")
public class User {
	@Id
	@Column(name = "id")
    @GeneratedValue(generator = "users_id_seq", strategy = GenerationType.SEQUENCE)
    @SequenceGenerator(name = "users_id_seq", sequenceName = "users_id_seq", allocationSize = 1)
	private Long userId;
    @Column(name = "first_name")
	private String firstName;
    @Column(name = "last_name")
	private String lastName;
    @Column(name = "password")
	private String password;
    @Column(name = "user_name")
	private String userName;
    @Column(name = "default_profile_id")
    private Long defaultProfileId;
    @Column(name="email")
    private String email;
    @Column(name="joined_on")
    private String joinedOn;
    @Column(name="country")
    private String country;
    @Column(name="phone")
    private String phone;

    public Long getUserId() {
        return userId;
    }

    public void setUserId(Long userId) {
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

    public Long getDefaultProfileId() {
        return defaultProfileId;
    }

    public void setDefaultProfileId(Long defaultProfileId) {
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

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof User)) return false;

        User user = (User) o;

        if (getDefaultProfileId() != user.getDefaultProfileId()) return false;
        if (getFirstName() != null ? !getFirstName().equals(user.getFirstName()) : user.getFirstName() != null)
            return false;
        if (getLastName() != null ? !getLastName().equals(user.getLastName()) : user.getLastName() != null)
            return false;
        if (!getPassword().equals(user.getPassword())) return false;
        if (!getUserName().equals(user.getUserName())) return false;
        if (getEmail() != null ? !getEmail().equals(user.getEmail()) : user.getEmail() != null) return false;
        if (!getJoinedOn().equals(user.getJoinedOn())) return false;
        if (getCountry() != null ? !getCountry().equals(user.getCountry()) : user.getCountry() != null) return false;
        return getPhone() != null ? getPhone().equals(user.getPhone()) : user.getPhone() == null;
    }

    @Override
    public int hashCode() {
        int result = getUserId().hashCode();
        result = 31 * result + (getFirstName() != null ? getFirstName().hashCode() : 0);
        result = 31 * result + (getLastName() != null ? getLastName().hashCode() : 0);
        result = 31 * result + getPassword().hashCode();
        result = 31 * result + getUserName().hashCode();
        result = 31 * result + getDefaultProfileId().hashCode();
        result = 31 * result + (getEmail() != null ? getEmail().hashCode() : 0);
        result = 31 * result + getJoinedOn().hashCode();
        result = 31 * result + (getCountry() != null ? getCountry().hashCode() : 0);
        result = 31 * result + (getPhone() != null ? getPhone().hashCode() : 0);
        return result;
    }

    public static final class UserBuilder {
        private Long userId;
        private String firstName;
        private String lastName;
        private String password;
        private String userName;
        private Long defaultProfileId;
        private String email;
        private String joinedOn;
        private String country;
        private String phone;

        private UserBuilder() {
        }

        public static UserBuilder aUser() {
            return new UserBuilder();
        }

        public UserBuilder withUserId(Long userId) {
            this.userId = userId;
            return this;
        }

        public UserBuilder withFirstName(String firstName) {
            this.firstName = firstName;
            return this;
        }

        public UserBuilder withLastName(String lastName) {
            this.lastName = lastName;
            return this;
        }

        public UserBuilder withPassword(String password) {
            this.password = password;
            return this;
        }

        public UserBuilder withUserName(String userName) {
            this.userName = userName;
            return this;
        }

        public UserBuilder withDefaultProfileId(Long defaultProfileId) {
            this.defaultProfileId = defaultProfileId;
            return this;
        }

        public UserBuilder withEmail(String email) {
            this.email = email;
            return this;
        }

        public UserBuilder withJoinedOn(String joinedOn) {
            this.joinedOn = joinedOn;
            return this;
        }

        public UserBuilder withCountry(String country) {
            this.country = country;
            return this;
        }

        public UserBuilder withPhone(String phone) {
            this.phone = phone;
            return this;
        }

        public User build() {
            User user = new User();
            user.setUserId(userId);
            user.setFirstName(firstName);
            user.setLastName(lastName);
            user.setPassword(password);
            user.setUserName(userName);
            user.setDefaultProfileId(defaultProfileId);
            user.setEmail(email);
            user.setJoinedOn(joinedOn);
            user.setCountry(country);
            user.setPhone(phone);
            return user;
        }
    }
}
