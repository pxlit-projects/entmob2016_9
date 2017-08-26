package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.Command;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CommandRepository extends CrudRepository<Command,Integer> {
}
