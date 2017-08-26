package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.Command;
import org.assertj.core.api.Assertions;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;

import static com.pxl.emotionjava.entities.CommandFixture.aCommand;

public class CommandRepositoryTest extends RepositoryTest{

    @Autowired
    private CommandRepository commandRepository;

    @Test
    public void commandPersistsCorrectly() throws Exception {
        Command command = aCommand();
        commandRepository.save(command);

        flushAndClear();

        Command actualCommand = commandRepository.findAll().iterator().next();

        Assertions.assertThat(actualCommand).isNotNull();
    }
}