package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.Action;
import org.assertj.core.api.Assertions;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;

import static com.pxl.emotionjava.entities.ActionFixture.anAction;

public class ActionRepositoryTest extends RepositoryTest{

    @Autowired
    private ActionRepository actionRepository;

    @Test
    public void actionPersistsCorrectly() throws Exception {
        Action action = anAction();
        actionRepository.save(action);

        flushAndClear();

        Action actualAction = actionRepository.findAll().iterator().next();

        Assertions.assertThat(actualAction).isNotNull();
    }
}