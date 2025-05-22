def water_column_height(tower_height, tank_height):
    """
    Calculate the height of a column of water.

    Parameters:
    - tower_height: float, height of the tower in meters
    - tank_height: float, height of the tank walls in meters

    Returns:
    - float: height of the water column in meters
    """
    return tower_height + (3 * tank_height / 4)


def pressure_gain_from_water_height(height):
    """
    Calculate the pressure gain from water height.

    Parameters:
    - height: float, height of the water column in meters

    Returns:
    - float: pressure in kilopascals (kPa)
    """
    density = 998.2      # kg/m^3
    gravity = 9.80665    # m/s^2
    return (density * gravity * height) / 1000
