package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.User;
import com.pxl.emotionjava.services.impl.UserDataServiceImpl;
import org.junit.Test;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import static com.pxl.emotionjava.entities.UserFixture.aUser;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.delete;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.put;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@WebMvcTest(UserController.class)
public class UserControllerTest extends ControllerTest{
    @MockBean
    private UserDataServiceImpl service;

    @Test
    @WithMockUser(username = "user")
    public void getUsers() throws Exception {
        List<User> users = new ArrayList<>();
        users.add(aUser());

        when(service.getAllUsers()).thenReturn(users);

        mvc.perform(get("/user/all")
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(users)));
    }
    
    @Test
    @WithMockUser(username = "user")
    public void getUserById() throws Exception {
        User user = aUser(1);

        when(service.getUserById(user.getUserId())).thenReturn(user);

        mvc.perform(get("/user/id/" + user.getUserId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(user)));
    }

    @Test
    @WithMockUser(username = "user")
    public void getUserByName() throws Exception {
        User user = aUser(1);

        when(service.getUserByName(user.getUserName())).thenReturn(Collections.singletonList(user));

        mvc.perform(get("/user/name/" + user.getUserName())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(Collections.singleton(user))));
    }

    @Test
    @WithMockUser(username = "user")
    public void addUser() throws Exception {
        User user = aUser();

        when(service.addOrUpdateUser(eq(user))).thenReturn("1");

        mvc.perform(post("/user/add").content(toJson(user))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.CREATED.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void deleteUser() throws Exception {
        User user = aUser(1);

        when(service.deleteUser(user.getUserId())).thenReturn("1");

        mvc.perform(delete("/user/delete/" + user.getUserId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void updateUser() throws Exception {
        User user = aUser(1);

        when(service.addOrUpdateUser(eq(user))).thenReturn("1");

        mvc.perform(put("/user/update").content(toJson(user))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void checkPass() throws Exception {
        User user = aUser(1);

        when(service.checkPass(eq(user))).thenReturn("1");

        mvc.perform(post("/user/pass").content(toJson(user))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }
}