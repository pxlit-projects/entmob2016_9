package be.pxl.emotion.repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import be.pxl.emotion.beans.User;

import java.util.List;

@Repository
public interface UserRepository extends CrudRepository<User,Integer>{
    List<User> findByUserName(String userName);
}
