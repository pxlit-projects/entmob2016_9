package be.pxl.emotion.repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import be.pxl.emotion.bean.User;

@Repository
public interface UserRepository extends CrudRepository<User,Integer>{

	User findById(int id);
}
