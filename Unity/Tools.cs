Using System;


namespace Peter28.Tools
{
      public const float Rad2Deg = 360.0 / (Mathf.PI * 2);
      public const float Deg2Rad = 1.0 / Rad2Deg;


      public float Vec2Angle(float dx, float dy)
      {
        if(dx < 0)
          return 360 - (Mathf.Atan2(dx, dy)* Rad2Deg * - 1);
        else
            return Math.Atan2(dx, dy) * Rad2Deg;
      }

      public Vector2 Angle2Vec2(float angle)
      {
         public float angleRad = angle / Deg2Rad;
          return Vector2(math.cos(angleRad, math.sin(angleRad));
      }
}
