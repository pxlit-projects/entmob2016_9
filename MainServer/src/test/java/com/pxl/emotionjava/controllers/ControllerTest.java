package com.pxl.emotionjava.controllers;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

@RunWith(SpringRunner.class)
public abstract class ControllerTest {
    protected static final String TOKEN = "Basic dXNlcjpwYXNzd29yZA=";

    protected Object[] urlVars = new Object[]{};

    @Autowired
    private ObjectMapper objectMapper;

    @Autowired
    protected MockMvc mvc;

    protected String toJson(Object data) throws JsonProcessingException {
        return objectMapper.writeValueAsString(data);
    }
}
