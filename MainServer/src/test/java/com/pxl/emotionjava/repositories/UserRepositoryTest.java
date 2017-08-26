package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.User;
import org.junit.Before;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

import static com.pxl.emotionjava.entities.UserFixture.aUser;
import static org.assertj.core.api.Assertions.assertThat;

public class UserRepositoryTest extends RepositoryTest{

    @Autowired
    private UserRepository userRepository;
    private User user;

    @Before
    public void setUp() throws Exception {
        user = aUser();
        userRepository.save(user);

        flushAndClear();
    }

    @Test
    public void userPersistsCorrectly() throws Exception {
        List<User> users = (List<User>) userRepository.findAll();

        assertThat(users).hasAtLeastOneElementOfType(User.class);

        User actualUser = users.get(users.size()-1);

        assertThat(actualUser).isNotNull();
    }

    @Test
    public void findByUserName() {
        List<User> users = userRepository.findByUserName(user.getUserName());

        assertThat(users).hasAtLeastOneElementOfType(User.class);

        User actualUser = users.get(users.size()-1);

        assertThat(actualUser).isEqualTo(user);
    }
}