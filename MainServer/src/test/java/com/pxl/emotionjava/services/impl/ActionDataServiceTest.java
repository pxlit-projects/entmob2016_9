package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Action;
import com.pxl.emotionjava.repositories.ActionRepository;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.ArrayList;
import java.util.List;

import static com.pxl.emotionjava.entities.ActionFixture.anAction;
import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.when;


@RunWith(SpringRunner.class)
public class ActionDataServiceTest {

    @Mock
    ActionRepository repository;

    @InjectMocks
    ActionDataServiceImpl actionService;

    @Test
    public void getActionById() throws Exception {
        Action action = anAction(1);
        when(repository.findOne(action.getActId())).thenReturn(action);

        Action actualAction = actionService.getActionById(action.getActId());

        assertThat(actualAction).isEqualTo(action);

    }

    @Test
    public void updateAction() throws Exception {
        Action action = anAction(1);
        when(repository.save(action)).thenReturn(action);

        Action actualAction = actionService.updateAction(action);

        assertThat(actualAction).isEqualTo(action);
    }

    @Test
    public void addAction() throws Exception {
        Action action = anAction(1);
        when(repository.save(action)).thenReturn(action);

        Action actualAction = actionService.addAction(action);

        assertThat(actualAction).isEqualTo(action);
    }

    @Test
    public void deleteAction() throws Exception {
        Action action = anAction(1);
        when(repository.exists(action.getActId())).thenReturn(true);

        String retVal = actionService.deleteAction(action.getActId());

        assertThat(retVal).isEqualTo("1");

        Mockito.verify(repository).delete(action.getActId());
    }

    @Test
    public void getAllActions() throws Exception {
        List<Action> actions = new ArrayList<>();
        actions.add(anAction(1));
        when(repository.findAll()).thenReturn(actions);

        List <Action> actualActions = actionService.getAllActions();

        assertThat(actualActions).hasSize(1);
        assertThat(actualActions.get(0)).isEqualTo(actions.get(0));
    }

}