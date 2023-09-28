using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;  // 角色的移动速度

    private Rigidbody rb;  // 角色的刚体组件

    private void Start()
    {
        // 获取角色的刚体组件
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 读取玩家的输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // 设置角色的速度
        rb.velocity = moveDirection * speed;
    }
}
