package com.turncount.zanyzebra.repositories;

import com.turncount.zanyzebra.entities.Profile;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ProfileRepository extends CrudRepository<Profile,Integer> {
    List<Profile> findByUserId(int userId);
}
