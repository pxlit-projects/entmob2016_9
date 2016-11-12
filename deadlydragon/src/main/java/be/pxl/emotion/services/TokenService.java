package be.pxl.emotion.services;

import be.pxl.emotion.beans.User;

import javax.xml.bind.DatatypeConverter;
import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

/**
 * Created by Dragonites on 12/11/2016.
 */
public class TokenService {
    public String GenerateToken(User user) {
        StringBuilder bdata = null;
        bdata.append(user.getUserId());
        bdata.append(user.getRole());
        String data = bdata.toString();
        MessageDigest digest = null;
        try {
            digest = MessageDigest.getInstance("SHA-256");
            byte[] hash = digest.digest(data.getBytes("UTF-8"));
            bdata = null;
            bdata.append(DatatypeConverter.printHexBinary(hash)).append('-').append(user.getUserName());
            return bdata.toString();
        } catch (Exception e) {
            return null;
        }
    }

    public boolean checkToken(String token){

    }
}
