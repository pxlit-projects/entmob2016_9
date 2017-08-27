package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.User;
import com.pxl.emotionjava.repositories.UserRepository;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import static com.pxl.emotionjava.entities.UserFixture.aUser;
import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.when;


@RunWith(SpringRunner.class)
public class UserDataServiceTest {

    @Mock
    private UserRepository repository;

    @InjectMocks
    private UserDataServiceImpl userService;
    private User user;

    @Before
    public void setUp() throws Exception {
        user = aUser(1L);
    }

    @Test
    public void getUserById() throws Exception {
        when(repository.findOne(user.getUserId())).thenReturn(user);

        User actualUser = userService.getUserById(user.getUserId());

        assertThat(actualUser).isEqualTo(user);
    }

    @Test
    public void getUserByName() throws Exception {
        when(repository.findByUserName(user.getUserName())).thenReturn(Collections.singletonList(user));

        List<User> actualUsers = userService.getUserByName(user.getUserName());

        assertThat(actualUsers).hasSize(1);
        assertThat(actualUsers.get(0)).isEqualTo(user);
    }

    @Test
    public void addOrUpdateUser() throws Exception {
        when(repository.save(eq(user))).thenReturn(user);
        when(repository.findByUserName(user.getUserName())).thenReturn(Collections.singletonList(user));

        String retVal = userService.addOrUpdateUser(user);

        assertThat(retVal).isEqualTo("1");
    }

    @Test
    public void deleteUser() throws Exception {
        when(repository.exists(user.getUserId())).thenReturn(true);

        String retVal = userService.deleteUser(user.getUserId());

        assertThat(retVal).isEqualTo("1");

        Mockito.verify(repository).delete(user.getUserId());
    }

    @Test
    public void getAllUsers() throws Exception {
        List<User> users = new ArrayList<>();
        users.add(user);
        when(repository.findAll()).thenReturn(users);

        List <User> actualUsers = userService.getAllUsers();

        assertThat(actualUsers).hasSize(1);
        assertThat(actualUsers.get(0)).isEqualTo(users.get(0));
    }

    @Test
    public void checkPass() throws Exception {
        user = aUser();
        when(repository.findByUserName(user.getUserName())).thenReturn(Collections.singletonList(user));

        String retVal = userService.checkPass(user);

        assertThat(retVal).isEqualTo("1");
    }
}