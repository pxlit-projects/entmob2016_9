package com.turncount.zanyzebra.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

 
@Controller
public class HomeController {
    @RequestMapping("/")
    @ResponseBody
    String home() {
        return "I'm a pwetty pwetty pwincess!";
    }
}

