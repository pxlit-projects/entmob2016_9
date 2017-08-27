package com.pxl.emotionjava.entities;

public class UserFixture {
    public static User aUser() {
        return User.UserBuilder.aUser()
                .withUserName("user")
                .withPassword("5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")
                .withDefaultProfileId(1L)
                .withCountry("Belgie")
                .withEmail("john.doe@email.com")
                .withFirstName("John")
                .withLastName("Doe")
                .withJoinedOn("2017-01-01 12:00:00.000000+02")
                .withPhone("012345678")
                .build();
    }

    public static User aUser(Long id) {
        User user = aUser();
        user.setUserId(id);
        return user;
    }
}
