package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.Profile;
import com.pxl.emotionjava.services.impl.ProfileDataServiceImpl;
import org.junit.Test;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;

import java.util.ArrayList;
import java.util.List;

import static com.pxl.emotionjava.entities.ProfileFixture.aProfile;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.delete;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.put;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@WebMvcTest(ProfileController.class)
public class ProfileControllerTest extends ControllerTest{
    @MockBean
    private ProfileDataServiceImpl service;

    @Test
    @WithMockUser(username = "user")
    public void getProfiles() throws Exception {
        List<Profile> profiles = new ArrayList<>();
        profiles.add(aProfile());

        when(service.getAllProfiles()).thenReturn(profiles);

        mvc.perform(get("/profile/all")
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(profiles)));
    }

    @Test
    @WithMockUser(username = "user")
    public void getProfilesByUserId() throws Exception {
        List<Profile> profiles = new ArrayList<>();
        profiles.add(aProfile());

        when(service.getProfilesByUserId(1L)).thenReturn(profiles);

        mvc.perform(get("/profile/userid/1")
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(profiles)));
    }

    @Test
    @WithMockUser(username = "user")
    public void getProfileById() throws Exception {
        Profile profile = aProfile(1L);

        when(service.getProfileById(profile.getProfileId())).thenReturn(profile);

        mvc.perform(get("/profile/id/" + profile.getProfileId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json(toJson(profile)));
    }

    @Test
    @WithMockUser(username = "user")
    public void addProfile() throws Exception {
        Profile profile = aProfile();

        when(service.addOrUpdateProfile(eq(profile))).thenReturn("1");

        mvc.perform(post("/profile/add").content(toJson(profile))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.CREATED.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void deleteProfile() throws Exception {
        Profile profile = aProfile(1L);

        when(service.deleteProfile(profile.getProfileId())).thenReturn("1");

        mvc.perform(delete("/profile/delete/" + profile.getProfileId())
                .header("Authorization", TOKEN))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }

    @Test
    @WithMockUser(username = "user")
    public void updateProfile() throws Exception {
        Profile profile = aProfile(1L);

        when(service.addOrUpdateProfile(eq(profile))).thenReturn("1");

        mvc.perform(put("/profile/update").content(toJson(profile))
                .header("Authorization", TOKEN)
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().is(HttpStatus.OK.value()))
                .andExpect(content().json("1"));
    }
}