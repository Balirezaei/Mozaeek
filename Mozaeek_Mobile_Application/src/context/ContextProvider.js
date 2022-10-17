import React, { useState } from 'react';
import AppContext from '.';

const ContextProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const context = {
        setUser,
        user,
    };

    return (
        <AppContext.Provider value={ context }>
            {children}
        </AppContext.Provider>
    );
};

export default ContextProvider;
