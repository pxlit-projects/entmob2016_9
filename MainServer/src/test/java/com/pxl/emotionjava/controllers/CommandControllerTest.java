package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.Command;
import com.pxl.emotionjava.services.impl.CommandDataServiceImpl;
import org.junit.Test;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;

import java.util.ArrayList;
import java.util.List;

import static com.pxl.emotionjava.entities.CommandFixture.aCommand;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.delete;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.put;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@WebMvcTest(CommandController.class)
public class CommandControllerTest extends ControllerTest {

    @MockBean
    private CommandDataServiceImpl service;

    @Test
    @WithMockUser(username = "user")
    public void getCommands() throws Exception {
        List<Command> commands = new ArrayList<>();
        commands.add(aCommand());

        when(service.getAllCommands()).thenReturn(commands);

        mvc.perform(get("/command/all")
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(commands)));
    }
    @Test
    @WithMockUser(username = "user")
    public void getCommandById() throws Exception {
        Command command = aCommand(1);

        when(service.getCommandById(command.getCommandId())).thenReturn(command);

        mvc.perform(get("/command/id/" + command.getCommandId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(command)));
    }

    @Test
    @WithMockUser(username = "user")
    public void addCommand() throws Exception {
        Command command = aCommand();

        when(service.addCommand(eq(command))).thenReturn(command);

        mvc.perform(post("/command/add").content(toJson(command))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.CREATED.value()))
                .andExpect(content().json(toJson(command)));
    }

    @Test
    @WithMockUser(username = "user")
    public void deleteCommand() throws Exception {
        Command command = aCommand(1);

        when(service.deleteCommand(command.getCommandId())).thenReturn("1");

        mvc.perform(delete("/command/delete/" + command.getCommandId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void updateCommand() throws Exception {
        Command command = aCommand(1);

        when(service.updateCommand(eq(command))).thenReturn(command);

        mvc.perform(put("/command/update/" + command.getCommandId()).content(toJson(command))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(command)));
    }
}