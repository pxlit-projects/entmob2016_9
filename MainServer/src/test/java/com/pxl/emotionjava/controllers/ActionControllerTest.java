package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.Action;
import com.pxl.emotionjava.services.impl.ActionDataServiceImpl;
import org.junit.Test;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;

import java.util.ArrayList;
import java.util.List;

import static com.pxl.emotionjava.entities.ActionFixture.anAction;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.delete;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.put;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@WebMvcTest(ActionController.class)
public class ActionControllerTest extends ControllerTest{

    @MockBean
    private ActionDataServiceImpl service;

    @Test
    @WithMockUser(username = "user")
    public void getActions() throws Exception {
        List<Action> actions = new ArrayList<>();
        actions.add(anAction());

        when(service.getAllActions()).thenReturn(actions);

        mvc.perform(get("/action/all")
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(actions)));
    }

    @Test
    @WithMockUser(username = "user")
    public void getActionById() throws Exception {
        Action action = anAction(1);

        when(service.getActionById(action.getActId())).thenReturn(action);

        mvc.perform(get("/action/id/" + action.getActId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(action)));
    }

    @Test
    @WithMockUser(username = "user")
    public void addAction() throws Exception {
        Action action = anAction();

        when(service.addAction(eq(action))).thenReturn(action);

        mvc.perform(post("/action/add").content(toJson(action))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.CREATED.value()))
                .andExpect(content().json(toJson(action)));
    }

    @Test
    @WithMockUser(username = "user")
    public void deleteAction() throws Exception {
        Action action = anAction(1);

        when(service.deleteAction(action.getActId())).thenReturn("1");

        mvc.perform(delete("/action/delete/" + action.getActId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void updateAction() throws Exception {
        Action action = anAction(1);

        when(service.updateAction(eq(action))).thenReturn(action);

        mvc.perform(put("/action/update/" + action.getActId()).content(toJson(action))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(action)));
    }
}