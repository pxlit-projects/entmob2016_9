package com.turncount.zanyzebra.controllers;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertEquals;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import javax.jms.ConnectionFactory;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.MockitoAnnotations;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.jms.DefaultJmsListenerContainerFactoryConfigurer;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.RequestBuilder;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import com.turncount.zanyzebra.entities.User;
import com.turncount.zanyzebra.services.UserDataService;

@RunWith(SpringRunner.class)
@WebMvcTest(value = UserController.class)
public class UserControllerTest {
	@Autowired
	private MockMvc mvc;
	
	@MockBean
	private UserDataService userService;
	@MockBean
	private ConnectionFactory myFactory;
	//@MockBean
	//private DefaultJmsListenerContainerFactoryConfigurer jmsListener;
	@MockBean
	private JmsTemplate jmsTemplate;
	
	User mockUser = new User(402, "Djuke");
	
	@Test
	public void getUserByIdTest() throws Exception {
		when(userService.getUserById(402)).thenReturn(mockUser);
		
		RequestBuilder requestBuilder = MockMvcRequestBuilders.get(
				"/user/id/402").accept(
				MediaType.APPLICATION_JSON);

		mvc.perform(requestBuilder)
		.andExpect(status().isOk())
		.andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
        .andExpect(jsonPath("$[0].id", is(402)))
		.andExpect(jsonPath("$[0].userName", is("Djuke")));
		
		
	}
	
}
