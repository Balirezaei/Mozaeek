const usePermissions = () => {
  const hasPermission = (permission: string) => {
    return undefined;
  };

  const hasPermissions = (permissions: readonly string[] | undefined, passIfEmpty: boolean = false) => {
    if (!permissions || permissions.length === 0) {
      return passIfEmpty;
    }

    for (const permission of permissions) {
      if (!hasPermission(permission)) {
        return false;
      }
    }
    return true;
  };

  return { hasPermission, hasPermissions };
};

export default usePermissions;
