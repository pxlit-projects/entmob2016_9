package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Command;
import com.pxl.emotionjava.repositories.CommandRepository;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.ArrayList;
import java.util.List;

import static com.pxl.emotionjava.entities.CommandFixture.aCommand;
import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.when;


@RunWith(SpringRunner.class)
public class CommandDataServiceTest {

    @Mock
    CommandRepository repository;

    @InjectMocks
    CommandDataServiceImpl commandService;

    @Test
    public void getCommandById() throws Exception {
        Command command = aCommand(1);
        when(repository.findOne(command.getCommandId())).thenReturn(command);

        Command actualCommand = commandService.getCommandById(command.getCommandId());

        assertThat(actualCommand).isEqualTo(command);

    }

    @Test
    public void updateCommand() throws Exception {
        Command command = aCommand(1);
        when(repository.save(command)).thenReturn(command);

        Command actualCommand = commandService.updateCommand(command);

        assertThat(actualCommand).isEqualTo(command);
    }

    @Test
    public void addCommand() throws Exception {
        Command command = aCommand(1);
        when(repository.save(command)).thenReturn(command);

        Command actualCommand = commandService.addCommand(command);

        assertThat(actualCommand).isEqualTo(command);
    }

    @Test
    public void deleteCommand() throws Exception {
        Command command = aCommand(1);
        when(repository.exists(command.getCommandId())).thenReturn(true);

        String retVal = commandService.deleteCommand(command.getCommandId());

        assertThat(retVal).isEqualTo("1");

        Mockito.verify(repository).delete(command.getCommandId());
    }

    @Test
    public void getAllCommands() throws Exception {
        List<Command> commands = new ArrayList<>();
        commands.add(aCommand(1));
        when(repository.findAll()).thenReturn(commands);

        List <Command> actualCommands = commandService.getAllCommands();

        assertThat(actualCommands).hasSize(1);
        assertThat(actualCommands.get(0)).isEqualTo(commands.get(0));
    }

}