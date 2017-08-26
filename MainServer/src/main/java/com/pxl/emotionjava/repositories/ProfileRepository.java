package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.Profile;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ProfileRepository extends CrudRepository<Profile,Integer> {
    List<Profile> findByUserId(int userId);
}
